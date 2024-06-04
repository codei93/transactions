<?php

namespace App\Livewire;

use Livewire\Component;
use Livewire\Attributes\Layout;

use Livewire\Attributes\Title;
use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Config;
use Illuminate\Support\Facades\Session;

class Dashboard extends Component
{
    // Properties
    public $defaultYear;
    public $backend_api_url = '';
    public $counUsers = 0;
    public $sumDeposits = 0;
    public $sumWithdraws = 0;

    public $years = [
        [
            "id" => 0,
            "value" => 2022,
        ],
        [
            "id" => 1,
            "value" => 2023,
        ],
        [
            "id" => 2,
            "value" => 2024,
        ],
        [
            "id" => 3,
            "value" => 2025,
        ]
    ];

    public array $transactionsGraph;


    // Mount function - runs when the component is initialized
    public function mount()
    {
        $this->backend_api_url = Config::get('app.backend_api_url.key');
        $this->years;
        $this->defaultYear = date('Y');
        $this->onFetchWidgets(); // Fetch initial data for widgets
        $this->onFetchGraph($this->defaultYear); // Fetch initial data for graph
        $this->onRedirect(); //redirect user to login screen if not authenticated
    }

    // Fetch data for widgets
    public function onFetchWidgets()
    {
        // HTTP requests to fetch data for users, deposits, and withdraws
        $response_users = Http::withHeaders([
            'Content-Type' => 'application/json',
            'Accept' => 'application/json',
            'Authorization' => "Bearer " . auth()->user()->bearer_token,
        ])->withoutVerifying()->get($this->backend_api_url . "/Analysis/count/users");

        $response_deposits = Http::withHeaders([
            'Content-Type' => 'application/json',
            'Accept' => 'application/json',
            'Authorization' => "Bearer " . auth()->user()->bearer_token,
        ])->withoutVerifying()->get($this->backend_api_url . "/Analysis/sum/deposits");

        $response_withdraws = Http::withHeaders([
            'Content-Type' => 'application/json',
            'Accept' => 'application/json',
            'Authorization' => "Bearer " . auth()->user()->bearer_token,
        ])->withoutVerifying()->get($this->backend_api_url . "/Analysis/sum/withdraws");

        $json_response_users = $response_users->json();
        $json_response_deposits = $response_deposits->json();
        $json_response_withdraws = $response_withdraws->json();

        $this->counUsers = $json_response_users['users']['userCount'];
        $this->sumDeposits = $json_response_deposits['deposits']['sumDeposits'];
        $this->sumWithdraws = $json_response_withdraws['withdraws']['sumWithdraws'];
    }

    // Fetch data for graph
    public function onFetchGraph($selectedYear)
    {
        $year = $selectedYear ? $selectedYear : $this->defaultYear;

        $response_graph = Http::withHeaders([
            'Content-Type' => 'application/json',
            'Accept' => 'application/json',
            'Authorization' => "Bearer " . auth()->user()->bearer_token,
        ])->withoutVerifying()->get($this->backend_api_url . "/Analysis/graph/" . $year);

        $json_response_graph = $response_graph->json();

        if ($response_graph->failed()) {
            $this->error(
                'Error',
                $json_response_graph['message'],
                position: 'toast-top toast-end',
                timeout: 10000,
            );
            return;
        }

        $grahpData = $json_response_graph['graphData']['monthlyTransactionCounts'];

        $this->transactionsGraph = [
            'type' => 'bar',
            'data' => [
                'labels' => ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                'datasets' => [
                    [
                        'label' => 'Month of Year',
                        //'data' => [12, 33, 3, 2, 44, 10, 14, 19, 40, 12, 19, 3],
                        'data' => $grahpData,
                        'backgroundColor' => '#EC5800'
                    ]
                ]
            ]
        ];
    }

    public function onRedirect()
    {
        // Check if user is authenticated
        if (Auth::check()) {
            return view('livewire.dashboard');
        }

        // If user is not authenticated, redirect to login page
        return $this->redirect('/');
    }


    // Render method - renders the Livewire component
    #[Title("Dashboard | Transactions")]
    #[Layout('components.layouts.app')]
    public function render()
    {
        return view('livewire.dashboard');
    }
}
