import React from "react";
import img1 from "../assets/black-tuxedo-28320936.jpg"
import img2 from "../assets/Ladies wearing-dresses.png"
import img3 from "../assets/little-girl-trying-out-new-jeans-sit_392895-182160.jpg"
const LandingBody: React.FC = () => {
  return (

    <section className="bg-brand-white text-brand-black py-12 px-6">
      <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
        {/* Left Promo Block */}

        <div className=" text-brand-white p-6 rounded-lg shadow-lg flex flex-col justify-between">
          <div  >

            <h2 className="text-3xl font-bold text-brand-black mb-2">30% Sale Off</h2>
            <p className="text-xl font-semibold  text-brand-black">All Ladies Wears </p>
            <div className="h-min w-max"> <img src={img2} /></div>

          </div>

         <div className=" flex justify-center flex-row py-0">
              <button className="mt-6 bg-brand-white text-brand-black  border-none font-semibold py-2 px-4 rounded hover:bg-yellow-400 transition">
                Shop Now
              </button>
            </div>
        </div>
        {/* Right Promo Grid */}
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-6">


          <div className="bg-white p-4 rounded-lg shadow-sm">

            <h3 className="text-lg font-bold text-brand-black">All Party Wears</h3>
            <p className="text-sm text-gray-600">
              <span> With Special Discount</span>
            </p>
            <div><img src={img1} /></div>
            <div className="mt-2">

              <span className="text-xl font-bold text-brand-black">$450</span>
              <span className="ml-2 line-through text-gray-500">$500</span>
            </div>

           <div className=" flex justify-center flex-row py-0">
              <button className="mt-6 bg-brand-white text-brand-black  border-none font-semibold py-2 px-4 rounded hover:bg-yellow-400 transition">
                Shop Now
              </button>
            </div>

          </div>

          <div className="bg-white  p-4 rounded-lg shadow-sm">

            <h3 className="text-lg font-bold text-brand-black">All Kids Wears with 40% off </h3>
            <p className="text-sm text-gray-600">Limited time offer</p>
            <div> <img src={img3} /></div>
            <div className="mt-2">

              <span className="text-xl font-bold text-brand-black">$600</span>
              <span className="ml-2 line-through text-gray-500">$899</span>
            </div>
            <div className=" flex justify-center flex-row py-0">
              <button className="mt-6 bg-brand-white text-brand-black  border-none font-semibold py-2 px-4 rounded hover:bg-yellow-400 transition">
                Shop Now
              </button>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};
export default LandingBody;
