<?php

namespace App\Livewire\Users;

use Livewire\Component;

use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Config;
use Livewire\Attributes\Title;


use Mary\Traits\Toast;

class UpdateUser extends Component
{
    use Toast;

    public $data;
    public $backend_api_url = '';
    public $id;
    public $username = '';
    public $email = '';
    public $roleId = '';
    public $roles = [];
    public function mount(int $id)
    {
        $this->backend_api_url = Config::get('app.backend_api_url.key');

        $this->onFetch($id);
        $this->onFetchRoles();

        $this->id = $id;
        $this->username = $this->data['username'];
        $this->email = $this->data['email'];
        $this->roleId = $this->data['roleId'];


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

    public function onFetch(int $id)
    {
        try {

            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
                'Authorization' => "Bearer " . auth()->user()->bearer_token,
            ])->withoutVerifying()->get($this->backend_api_url . "/Users/" . $id);

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

            $this->data = $json_response['user'];

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
            'id' => 'required',
            'username' => 'required|max:20',
            'email' => 'required|email',
            'roleId' => 'required',
        ]);

        try {
            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
                'Authorization' => "Bearer " . auth()->user()->bearer_token,
            ])->withoutVerifying()->put($this->backend_api_url . "/Users/" . $this->id, $validate);

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

            return $this->redirect('/users', navigate: true);

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
            ])->withoutVerifying()->delete($this->backend_api_url . "/Users/" . $id);

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

            return $this->redirect('/users', navigate: true);

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


    #[Title('Update User | Transactions')]
    public function render()
    {
        return view('livewire.users.update-user');
    }
}
