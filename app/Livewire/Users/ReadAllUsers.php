<?php

namespace App\Livewire\Users;

use Livewire\Component;
use Livewire\WithPagination;
use Livewire\Attributes\Title;

use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Config;
use Mary\Traits\Toast;


class ReadAllUsers extends Component
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
            ['key' => 'username', 'label' => 'Username', 'class' => 'w-72'],
            ['key' => 'email', 'label' => 'Email', 'class' => 'w-72'],
            ['key' => 'role.name', 'label' => 'Role', 'class' => 'w-72'],

        ];
    }

    public function onFetch()
    {
        try {

            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
                'Authorization' => "Bearer " . auth()->user()->bearer_token,
            ])->withoutVerifying()->get($this->backend_api_url . "/Users");

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

            $collection = collect($json_response['users']);

            if ($this->search) {
                $this->data = $collection->filter(function ($item) {
                    return stripos($item['username'], $this->search) !== false;
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

    #[Title('Users | Transactions')]
    public function render()
    {
        $this->onFetch();
        return view('livewire.users.read-all-users');
    }
}
