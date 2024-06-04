/** @type {import('tailwindcss').Config} */
export default {
  daisyui: {
    themes: ["light", "black", "forest"],
  },
  content: [
    "./resources/**/**/*.blade.php",
     "./resources/**/**/*.js",
    "./resources/**/*.vue",
    "./app/View/Components/**/**/*.php",
    "./app/Livewire/**/**/*.php",
    "./vendor/robsontenorio/mary/src/View/Components/**/*.php",
    './vendor/laravel/framework/src/Illuminate/Pagination/resources/views/*.blade.php',
  ],
  theme: {
    extend: {},
  },
  plugins: [require("daisyui")],
}

