<?php

namespace App\Livewire\Auth;

use Livewire\Component;
use Livewire\Attributes\Layout;
use Livewire\Attributes\Title;

use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Config;

use App\Models\User;
use Mary\Traits\Toast;

class Register extends Component
{
    use Toast;

    public $backend_api_url = '';
    public $username;
    public $email;
    public $password;
    public $roleId;

    public function mount()
    {
        $this->backend_api_url = Config::get('app.backend_api_url.key');
        $this->roleId = 2;
    }

    public function onSubmit()
    {
        $validate = $this->validate([
            'username' => 'required|max:20',
            'email' => 'required|email',
            'password' => 'required',
            "roleId" => 'required'
        ]);

        try {
            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
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

            // Redirect to the dashboard
            return $this->redirect('/', navigate: true);

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

    #[Title('Register | Transactions')]
    #[Layout('components.layouts.auth')]
    public function render()
    {
        return view('livewire.auth.register');
    }
}
