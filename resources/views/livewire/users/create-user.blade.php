<div class="md:bg-base-200 p-6 shadow-lg w-full">

    <!-- Header component for "Create User" -->
    <x-header title="Create User" />

    <!-- Header component with separator and progress indicator -->
    <x-header size="text-inherit" separator progress-indicator>

    </x-header>

    <!-- Card component with form for creating user -->
    <x-card class="mt-10 !p-0 sm:!p-2 flex justify-center items-center" shadow>
        <div class="max-w-lg">
            <!-- Form component with wire:submit action -->
            <x-form wire:submit="onSubmit">
                <!-- Input field for username -->
                <x-input label="Username" value="" wire:model="username" icon="o-user" inline />
                <!-- Input field for email -->
                <x-input label="Email" value="" wire:model="email" icon="o-at-symbol" inline />
                <!-- Input field for password -->
                <x-input label="Password" value="" wire:model="password" type="password" icon="o-key" inline />
                <!-- Radio buttons for selecting role -->
                <x-radio label="Select Role" :options="$roles" option-value="id" option-label="name"
                    wire:model="roleId" />
                <!-- Action buttons for canceling and saving -->
                <x-slot:actions>
                    <x-button label="Cancel" type="button" icon="o-arrow-left" link="/users" class="btn-ghost" />
                    <x-button label="Save" type="submit" icon="o-paper-airplane" class="btn-primary"
                        spinner="onSubmit" />
                </x-slot:actions>
            </x-form>
        </div>
    </x-card>
</div>
