<?php

namespace App\Livewire\Settings;

use Livewire\Component;
use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Config;
use Livewire\Attributes\Title;


use Mary\Traits\Toast;

class UpdatePassword extends Component
{

    use Toast;

    public $backend_api_url = '';
    public $username;
    public $currentPassword;
    public $password;

    public function mount()
    {
        $this->backend_api_url = Config::get('app.backend_api_url.key');
        $this->username = auth()->user()->name;
    }

    public function onSubmit()
    {
        $validate = $this->validate([
            'username' => 'required',
            'currentPassword' => 'required',
            'password' => 'required',
        ]);

        try {
            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
                'Authorization' => "Bearer " . auth()->user()->bearer_token,
            ])->withoutVerifying()->put($this->backend_api_url . "/Auth/updatePassword", $validate);

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

            return $this->redirect('/logout', navigate: true);

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

    #[Title("Update Password | Transactions")]
    public function render()
    {
        return view('livewire.settings.update-password');
    }
}
