<?php

namespace App\Livewire\Transactions;

use Livewire\Component;
use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Config;
use Livewire\Attributes\Title;

use Mary\Traits\Toast;

class CreateTransaction extends Component
{
    use Toast;

    public $backend_api_url = '';
    public $customerNames;
    public $transactionType;
    public $amount;
    public $description;
    public $paymentType;

    public $transactionTypeData = [
        [
            "id" => 0,
            "value" => "Deposit"
        ],
        [
            "id" => 1,
            "value" => "Withdraw"
        ]
    ];

    public $paymentTypeData = [
        [
            "id" => 0,
            "value" => "Mobile Money"
        ],
        [
            "id" => 2,
            "value" => "Visa Card"
        ],
        [
            "id" => 3,
            "value" => "Bank Transfer"
        ]
    ];

    public function mount()
    {
        $this->backend_api_url = Config::get('app.backend_api_url.key');
        $this->transactionTypeData;
        $this->paymentTypeData;
    }

    public function onSubmit()
    {
        $validate = $this->validate([
            'customerNames' => 'required|max:50',
            'transactionType' => 'required',
            'amount' => 'required|numeric|min:1',
            'description' => 'required',
            'paymentType' => 'required',
        ]);

        try {

            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
                'Authorization' => "Bearer " . auth()->user()->bearer_token,
            ])->withoutVerifying()->post($this->backend_api_url . "/Transactions", $validate);

            $json_response = $response->json();

            if ($response->failed()) {
                $this->error(
                    'Error',
                    $json_response['message'],
                    position: 'toast-top toast-end',
                    timeout: 10000,
                );
                return;
            }

            $this->success(
                'Success',
                $json_response['message'],
                position: 'toast-top toast-end',
                timeout: 10000,
            );

            return $this->redirect("/transactions", navigate: true);

        } catch (\Exception $e) {
            // Handle exceptions
            $this->error(
                'Error',
                $e->getMessage(),
                position: 'toast-top toast-end',
                timeout: 10000,
            );
        }
    }

    #[Title('Create Transaction | Transactions')]
    public function render()
    {
        return view('livewire.transactions.create-transaction');
    }
}
