import axios from "axios";
import { toast } from "react-toastify";
import { history } from "..";

axios.defaults.baseURL = "http://localhost:5000/api";

axios.interceptors.request.use(
  (config) => {
    const token = window.localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axios.interceptors.response.use(undefined, (error) => {
  console.log(error.response);
  if (error.message === "Network Error" && !error.response) {
    toast.error("Network Error");
    return;
  }
  if (
    (error.response.status === 401 || error.response.status === 403) &&
    error.response.config.method === "get"
  ) {
    history.push("/unauthorized");
    return;
  }
  if (error.response.status === 500) {
    toast.error("Server error");
  }
  if (error.response.status === 404) {
    history.push("/notfound");
  }
  if (
    error.response.status === 400 &&
    error.response.config.method === "get" &&
    error.response.data.errors.hasOwnProperty("id")
  ) {
    history.push("/notfound");
  }

  throw error.response;
});

const responseBody = (response) => {
  if (response != undefined) {
    return response.data;
  }
  return null;
};

const sleep = (ms) => (response) =>
  new Promise((resolve) => setTimeout(() => resolve(response), ms));

const requests = {
  get: (url) => axios.get(url).then(sleep(1000)).then(responseBody),
  post: (url, body) =>
    axios.post(url, body).then(sleep(1000)).then(responseBody),
  put: (url, body) => axios.put(url, body).then(sleep(1000)).then(responseBody),
  delete: (url) => axios.delete(url).then(sleep(1000)).then(responseBody),
};

const User = {
  current: () => requests.get("/user"),
  login: (user) => requests.post("/user/login", user),
  register: (user) => requests.post("/user/register", user),
};

const Drink = {
  get: (id) => requests.get(`/drinks/${id}`),
};

const Reviews = {
  get: (id) => requests.get(`/drinks/${id}/reviews`),
  post: (id, review) => requests.post(`/drinks/${id}/reviews`, review),
};

const agent = {
  requests,
  User,
  Drink,
  Reviews,
};
export default agent;
