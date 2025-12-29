import { Routes,Route } from "react-router-dom";
import LandingPage from "./landingPage/LandingPage";
import SignIn from "./SignIn";
import SignUp from "./SignUp";

function App() {
  return (
   <Routes>
    <Route path = "/" element = {<LandingPage/>} />
    <Route path = "/signup" element = {<SignUp/>} />
    <Route path = "Signin" element = {<SignIn/>} />
   </Routes>
  );
}

export default App
