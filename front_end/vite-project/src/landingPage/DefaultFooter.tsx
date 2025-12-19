import React from "react";

const DefaultFooter: React.FC = () => {
  return (
    <footer className="bg-brand.black text-brand.white py-10">
      <div className="container mx-auto px-6 grid grid-cols-1 md:grid-cols-3 gap-8">
        
        {/* Brand Section */}
        <div>
          <h2 className="text-2xl font-bold text-brand.yellow">AnantaMall</h2>
          <p className="mt-2 text-sm text-gray-300">
            Your eternal marketplace for modern shopping.
          </p>
        </div>

        {/* Navigation Links */}
        <div>
          <h3 className="text-lg font-semibold mb-4 text-brand.yellow">Quick Links</h3>
          <ul className="space-y-2">
            <li><a href="#about" className="hover:text-brand.yellow">About</a></li>
            <li><a href="#contact" className="hover:text-brand.yellow">Contact Us</a></li>
            <li><a href="#signin" className="hover:text-brand.yellow">Sign In</a></li>
            <li><a href="#register" className="hover:text-brand.yellow">Register</a></li>
          </ul>
        </div>

        {/* Social Media */}
        <div>
          <h3 className="text-lg font-semibold mb-4 text-brand.yellow">Follow Us</h3>
          <div className="flex space-x-4">
            <a href="#" className="hover:text-brand.yellow">
              <svg className="w-6 h-6" fill="currentColor" viewBox="0 0 24 24">...</svg>
            </a>
            <a href="#" className="hover:text-brand.yellow">
              <svg className="w-6 h-6" fill="currentColor" viewBox="0 0 24 24">...</svg>
            </a>
            <a href="#" className="hover:text-brand.yellow">
              <svg className="w-6 h-6" fill="currentColor" viewBox="0 0 24 24">...</svg>
            </a>
          </div>
        </div>
      </div>

      {/* Bottom Bar */}
      <div className="mt-8 border-t border-brand.yellow pt-4 text-center text-sm text-gray-400">
        © {new Date().getFullYear()} AnantaMall. All rights reserved.
      </div>
    </footer>
  );
};

export default DefaultFooter;
