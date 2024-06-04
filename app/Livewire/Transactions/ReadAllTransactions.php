<?php

namespace App\Livewire\Transactions;

use Livewire\Component;
use Livewire\WithPagination;
use Livewire\Attributes\Title;

use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Config;
use Illuminate\Support\Collection;
use Illuminate\Pagination\LengthAwarePaginator;

use Mary\Traits\Toast;

class ReadAllTransactions extends Component
{
    use Toast;
    use WithPagination;

    public $backend_api_url = '';
    public $response;
    public $data = [];
    public $headers = [];

    public $search = '';
    public function mount()
    {
        $this->backend_api_url = Config::get('app.backend_api_url.key');
        $this->headers = [
            ['key' => 'id', 'label' => '#', 'class' => 'w-1'],
            ['key' => 'customerNames', 'label' => 'Customer', 'class' => 'w-72'],
            ['key' => 'transactionType', 'label' => 'Transaction Type', 'class' => 'w-72'],
            ['key' => 'amount', 'label' => 'Amount', 'class' => 'w-72'],
            ['key' => 'paymentType', 'label' => 'Payment Type', 'class' => 'w-72'],
            ['key' => 'user.username', 'label' => 'Created By', 'class' => 'w-72'],
            ['key' => 'createdAt', 'label' => 'Created At', 'class' => 'w-72'],
        ];
    }

    public function onFetch()
    {
        try {

            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
                'Authorization' => "Bearer " . auth()->user()->bearer_token,
            ])->withoutVerifying()->get($this->backend_api_url . "/Transactions");

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

            $collection = collect($json_response['transactions']);

            if ($this->search) {
                $this->data = $collection->filter(function ($item) {
                    return stripos($item['customerNames'], $this->search) !== false;
                });
            } else {
                $this->data = $collection;
            }

        } catch (\Exception $e) {
            $this->error(
                'Error',
                $e->getMessage(),
                position: 'toast-top toast-end',
                timeout: 10000,
            );
        }
    }


    #[Title('Transactions | Transactions')]
    public function render()
    {
        $this->onFetch();

        return view('livewire.transactions.read-all-transactions');
    }
}
