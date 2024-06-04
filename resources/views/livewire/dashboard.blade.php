<div>
    <!-- Header component for "Analytics" -->
    <x-header title="Analytics" />

    <!-- Grid layout for cards -->
    <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4 mb-10">
        <!-- Card component for "Users" -->
        <x-card title="Users" subtitle="Total Number of users" class="bg-base-200">
            <div class="flex items-center justify-between">
                <!-- Icon for users -->
                <x-icon name="o-user-plus" class="w-9 h-9 text-orange-500" />
                <div>
                    <div class="text-2xl font-bold">
                        <!-- Display total number of users -->
                        @php
                            echo number_format($counUsers, 2);
                        @endphp
                    </div>
                </div>
            </div>
        </x-card>

        <!-- Card component for "Deposits" -->
        <x-card title="Deposits" subtitle="Sum of deposits" class="bg-base-200">
            <div class="flex items-center justify-between">
                <!-- Icon for deposits -->
                <x-icon name="o-plus-circle" class="w-9 h-9 text-green-500" />
                <div>
                    <div class="text-2xl font-bold">
                        <!-- Display sum of deposits -->
                        @php
                            echo 'UGX' . number_format($sumDeposits, 2);
                        @endphp
                    </div>
                </div>
            </div>
        </x-card>

        <!-- Card component for "Withdraw" -->
        <x-card title="Withdraw" subtitle="Sum of withdraws" class="bg-base-200">
            <div class="flex items-center justify-between">
                <!-- Icon for withdraws -->
                <x-icon name="o-minus-circle" class="w-9 h-9 text-red-500" />
                <div>
                    <div class="text-2xl font-bold">
                        <!-- Display sum of withdraws -->
                        @php
                            echo 'UGX' . number_format($sumWithdraws, 2);
                        @endphp
                    </div>
                </div>
            </div>
        </x-card>
    </div>

    <!-- Card component for "Transactions" -->
    <x-card title="Transactions" subtitle="Monthly total number of transactions by year" class="bg-base-200">
        <!-- Header component with select input for selecting year -->
        <x-header title="" subtitle="">
            <x-slot:actions>
                <!-- Select input for selecting year -->
                <x-select label="" :options="$years" option-value="value" option-label="value"
                    placeholder="Select Year" placeholder-value="2022" wire:model="defaultYear"
                    wire:click="onFetchGraph($event.target.value)" />
            </x-slot:actions>
        </x-header>
        <!-- Chart component for displaying transactions graph -->
        <x-chart wire:model="transactionsGraph" />
    </x-card>
</div>
