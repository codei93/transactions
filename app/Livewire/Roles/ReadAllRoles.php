<?php

namespace App\Livewire\Roles;

use Livewire\Component;
use Livewire\WithPagination;
use Livewire\Attributes\Title;

use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Config;
use Mary\Traits\Toast;

class ReadAllRoles extends Component
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
            ['key' => 'name', 'label' => 'Role Name', 'class' => 'w-72'],
            ['key' => 'createdAt', 'label' => 'Created At', 'class' => 'w-72'],
            ['key' => 'updatedAt', 'label' => 'Updated At', 'class' => 'w-72'],

        ];

    }

    public function onFetch()
    {
        try {

            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
                'Authorization' => "Bearer " . auth()->user()->bearer_token,
            ])->withoutVerifying()->get($this->backend_api_url . "/Roles");

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

            $collection = collect($json_response['roles']);

            if ($this->search) {
                $this->data = $collection->filter(function ($item) {
                    return stripos($item['name'], $this->search) !== false;
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


    #[Title('Roles | Transactions')]
    public function render()
    {
        $this->onFetch();
        return view('livewire.roles.read-all-roles');
    }
}
