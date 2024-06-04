<?php

namespace App\Livewire\Transactions;

use Livewire\Component;
use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Config;
use Livewire\Attributes\Title;

use Mary\Traits\Toast;

class UpdateTransaction extends Component
{
    use Toast;

    public $id;
    public $data;
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


    public function mount(int $id)
    {
        $this->backend_api_url = Config::get('app.backend_api_url.key');
        $this->id = $id;
        $this->onFetch($id);
        $this->transactionTypeData;
        $this->paymentTypeData;

        $this->customerNames = $this->data['customerNames'];
        $this->transactionType = $this->data['transactionType'];
        $this->amount = $this->data['amount'];
        $this->description = $this->data['description'];
        $this->paymentType = $this->data['paymentType'];
    }

    public function onFetch(int $id)
    {
        try {

            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
                'Authorization' => "Bearer " . auth()->user()->bearer_token,
            ])->withoutVerifying()->get($this->backend_api_url . "/Transactions/" . $id);

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

            $this->data = $json_response['transaction'];

        } catch (\Exception $e) {
            $this->error(
                'Error',
                $e->getMessage(),
                position: 'toast-top toast-end',
                timeout: 10000,
            );
        }
    }

    public function onSubmit()
    {
        $validate = $this->validate([
            'customerNames' => 'required|max:50',
            'transactionType' => 'required',
            'amount' => 'required|numeric|min:1',
            'description' => 'required',
            'paymentType' => 'required',
            'id' => 'required'
        ]);

        try {

            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
                'Authorization' => "Bearer " . auth()->user()->bearer_token,
            ])->withoutVerifying()->put($this->backend_api_url . "/Transactions/" . $this->id, $validate);

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

    public function onDelete($id)
    {
        try {
            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
                'Authorization' => "Bearer " . auth()->user()->bearer_token,
            ])->withoutVerifying()->delete($this->backend_api_url . "/Transactions/" . $id);

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

            return $this->redirect('/transactions', navigate: true);

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

    #[Title('Details | Transactions')]
    public function render()
    {
        return view('livewire.transactions.update-transaction');
    }
}
