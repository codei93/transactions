<?php

namespace App\Livewire\Auth;

use Livewire\Component;
use Livewire\Attributes\Title;
use Livewire\Attributes\Layout;

use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Config;

use App\Models\User;
use Illuminate\Support\Facades\Session;
use Mary\Traits\Toast;

class Login extends Component
{
    use Toast;

    public $backend_api_url = '';
    public $username = '';
    public $password = '';

    public function mount()
    {
        $this->backend_api_url = Config::get('app.backend_api_url.key');
    }

    public function onSubmit()
    {
        $validate = $this->validate([
            'username' => 'required|max:20',
            'password' => 'required'
        ]);

        try {
            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
            ])->withoutVerifying()->post($this->backend_api_url . "/Auth/login", $validate);

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

            $userData = $json_response['user'];
            $userToken = $json_response['token'];

            // Find or create the user in the local database
            $user = User::updateOrCreate(
                ['email' => $userData['email']],
                [
                    'name' => $userData['username'],
                    'password' => bcrypt($validate['password']),
                    'bearer_token' => $userToken,
                    'role' => $userData['role']
                ]
            );

            // Log the user in
            Auth::login($user);

            // Redirect to the dashboard
            return $this->redirect('/dashboard');

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

    public function onLogOut()
    {
        Session::flush();
        Auth::logout();
        $this->js('window.location.reload()');
    }


    #[Title('Login | Transactions')]
    #[Layout('components.layouts.auth')]
    public function render()
    {
        return view('livewire.auth.login');
    }
}
