<?php

namespace App\Livewire\Roles;

use Livewire\Component;
use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Config;
use Livewire\Attributes\Title;


use Mary\Traits\Toast;

class UpdateRole extends Component
{
    use Toast;

    public $data;
    public $backend_api_url = '';
    public $id;
    public $name;
    public function mount(int $id)
    {
        $this->backend_api_url = Config::get('app.backend_api_url.key');
        $this->onFetch($id);
        $this->id = $id;
        $this->name = $this->data['name'];
    }

    public function onFetch(int $id)
    {
        try {

            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
                'Authorization' => "Bearer " . auth()->user()->bearer_token,
            ])->withoutVerifying()->get($this->backend_api_url . "/Roles/" . $id);

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

            $this->data = $json_response['role'];

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
            'name' => 'required|max:20',
        ]);

        try {
            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
                'Accept' => 'application/json',
                'Authorization' => "Bearer " . auth()->user()->bearer_token,
            ])->withoutVerifying()->put($this->backend_api_url . "/Roles/" . $this->id, $validate);

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

            return $this->redirect('/roles', navigate: true);

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
            ])->withoutVerifying()->delete($this->backend_api_url . "/Roles/" . $id);

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

            return $this->redirect('/roles', navigate: true);

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

    #[Title('Update Role | Transactions')]
    public function render()
    {
        return view('livewire.roles.update-role');
    }


}
