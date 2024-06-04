<?php

namespace App\Livewire\Roles;

use Livewire\Component;
use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Config;
use Livewire\Attributes\Title;

use Mary\Traits\Toast;

class CreateRole extends Component
{
    use Toast;
    public $app_name = '';
    public $backend_api_url = '';
    public $name = '';
    public function mount()
    {
        $this->app_name = Config::get('app.application_name.key');
        $this->backend_api_url = Config::get('app.backend_api_url.key');
    }

    public function onSubmit()
    {
        $validate = $this->validate([
            'name' => 'required|max:20',
        ]);

        try {
            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
                'Authorization' => "Bearer " . auth()->user()->bearer_token,
            ])->withoutVerifying()->post($this->backend_api_url . "/Roles", $validate);

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

    #[Title("Create Role | Transactions")]
    public function render()
    {
        return view('livewire.roles.create-role');
    }
}
