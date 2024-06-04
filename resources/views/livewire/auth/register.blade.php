<div>
    <!-- Header component -->
    <x-header title="Register" subtitle="Fill in to register as an agent" with-anchor />

    <!-- Form component -->
    <x-form wire:submit="onSubmit">
        <!-- Username input -->
        <x-input label="Username" value="" wire:model="username" icon="o-user" inline />
        <!-- Email input -->
        <x-input label="Email" value="" wire:model="email" icon="o-user" inline />
        <!-- Password input -->
        <x-input label="Password" value="" wire:model="password" type="password" icon="o-key" inline />

        <!-- Header component for "Have an account?" -->
        <x-header subtitle="Have an account? " with-anchor>
            <!-- Action button -->
            <x-slot:actions>
                <x-button label="Login Here" type="button" icon="o-arrow-right" class="btn-ghost" link="/" />
            </x-slot:actions>
        </x-header>

        <!-- Submit button -->
        <x-slot:actions>
            <x-button label="Register" type="submit" icon="o-paper-airplane" class="btn-primary" spinner="onSubmit" />
        </x-slot:actions>
    </x-form>
</div>
