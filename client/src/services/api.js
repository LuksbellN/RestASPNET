import axios from "axios";

const api = axios.create();
api.defaults.baseURL = "https://localhost:7015/";

export default api;