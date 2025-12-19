import React from "react";

const LandingBody: React.FC = () => {
  return (
    <section className="bg-brand.white text-brand.black py-12 px-6">
      <div className="grid grid-cols-1 md:grid-cols-2 gap-8">

        {/* Left Promo Block */}
        <div className="bg-brand.black text-brand.white p-6 rounded-lg shadow-lg flex flex-col justify-between">
          <div>
            <h2 className="text-3xl font-bold text-brand.yellow mb-2">30% Sale Off</h2>
            <p className="text-xl font-semibold">Macbook Pro M4 Pro - 512/16GB</p>
            <p className="mt-2 text-sm text-gray-300">
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi at ipsum at risus euismod lobortis.
            </p>
          </div>
          <button className="mt-6 bg-brand.yellow text-brand.black font-semibold py-2 px-4 rounded hover:bg-yellow-400 transition">
            Shop Now
          </button>
        </div>

        {/* Right Promo Grid */}
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-6">
          {/* Macbook Deal */}
          <div className="bg-white border border-brand.black p-4 rounded-lg shadow-sm">
            <h3 className="text-lg font-bold text-brand.black">Macbook Pro - 512/16GB</h3>
            <p className="text-sm text-gray-600">Limited time offer</p>
            <div className="mt-2">
              <span className="text-xl font-bold text-brand.yellow">$450</span>
              <span className="ml-2 line-through text-gray-500">$500</span>
            </div>
          </div>

          {/* iPhone Deal */}
          <div className="bg-white border border-brand.black p-4 rounded-lg shadow-sm">
            <h3 className="text-lg font-bold text-brand.black">iPhone 16 Pro - 8/128GB</h3>
            <p className="text-sm text-gray-600">Limited time offer</p>
            <div className="mt-2">
              <span className="text-xl font-bold text-brand.yellow">$600</span>
              <span className="ml-2 line-through text-gray-500">$899</span>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default LandingBody;
