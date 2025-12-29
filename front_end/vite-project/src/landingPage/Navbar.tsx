import React from "react";
import logo3 from "../assets/Copilot_20251219_182204 (1).png"
const Navbar: React.FC = () => {
  return (
    <nav className="bg-black text-white px-6 py-4 shadow-md">
      <div className="container mx-auto flex justify-between items-center">
        {/* Logo */}
        <div className="flex items-center h-12">
          <span className="ml-3 text-xl font-bold text-yellow-500">
            <img src={logo3} alt="AnantMall Logo" className="h-24 w-auto" />
            </span>
        </div>

        {/* Navigation Links */}
        <ul className="hidden md:flex space-x-6">
          <li><a href="#about" className="hover:text-yellow-400 transition">About</a></li>
          <li><a href="#contact" className="hover:text-yellow-400 transition">Contact Us</a></li>
          <li><a href="#signin" className="hover:text-yellow-400 transition">Sign In</a></li>
          <li><a href="#register" className="hover:text-yellow-400 transition">Register</a></li>
        </ul>

        {/* Mobile Menu Button */}
        <button className="md:hidden focus:outline-none">
          <svg className="w-6 h-6 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2}
              d="M4 6h16M4 12h16M4 18h16" />
          </svg>
        </button>
      </div>
    </nav>
  );
};

export default Navbar;
