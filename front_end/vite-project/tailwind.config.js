// tailwind.config.js or tailwind.config.ts
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        brand: {
          yellow: "#FFD700",
          black: "#1a1a1a",
          white: "#ffffff",
        },
      },
    },
  },
  plugins: [],
};
