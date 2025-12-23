import axios from "axios";
const api= axios.create({
    baseURL : "https://localhost:7291/api",
    headers :{
        "content-Type" : "application/json",
    },
    withCredentials :true,
});

export default api;