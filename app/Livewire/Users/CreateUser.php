<?php

namespace App\Livewire\Users;

use Livewire\Component;
use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Config;
use Livewire\Attributes\Title;

use Mary\Traits\Toast;

class CreateUser extends Component
{
    use Toast;

    public $backend_api_url = '';
    public $username = '';
    public $email = '';
    public $password = '';
    public $roleId = '';
    public $roles = [];

    public function mount()
    {
        $this->backend_api_url = Config::get('app.backend_api_url.key');
        $this->onFetchRoles();
    }

    public function onFetchRoles()
    {
        $response_roles = Http::withHeaders([
            'Content-Type' => 'application/json',
            'Accept' => 'application/json',
            'Authorization' => "Bearer " . auth()->user()->bearer_token,
        ])->withoutVerifying()->get($this->backend_api_url . "/Roles");

        $json_response_roles = $response_roles->json();

        if ($response_roles->failed()) {
            $this->error(
                'Error',
                $json_response_roles['message'],
                position: 'toast-top toast-end',
                timeout: 10000,
            );
            return;
        }

        $this->roles = $json_response_roles['roles'];

    }

    public function onSubmit()
    {
        $validate = $this->validate([
            'username' => 'required|max:20',
            'email' => 'required|email',
            'password' => 'required',
            'roleId' => 'required',
        ]);

        try {

            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
                'Authorization' => "Bearer " . auth()->user()->bearer_token,
            ])->withoutVerifying()->post($this->backend_api_url . "/Auth/register", $validate);

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

            $this->reset();

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

    #[Title('Create User | Transactions')]

    public function render()
    {
        return view('livewire.users.create-user');
    }
}
