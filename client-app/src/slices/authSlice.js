import { createSlice } from "@reduxjs/toolkit";
import agent from "../api/agent";
import { history } from "..";
import {toast} from "react-toastify";

const authSlice = createSlice({
  name: "auth",
  initialState: { currentUser: {}, token: window.localStorage.getItem("token"), isLogged: false },
  reducers: {
    signIn: (state, action) => {
      state.currentUser = action.payload;
    },
    setIsLogged: (state, action) => {
      state.isLogged = action.payload;
    },
    setToken: (state, action) => {
      state.token = action.payload;
    },
    logout: (state) =>{
      window.localStorage.clear();
      state.currentUser = {};
      state.token = null;
      state.isLogged = false;
      history.push("/");
    }
  },
});

export const { signIn, logout, setIsLogged, setToken } = authSlice.actions;

export const signInAsync = (values) => async (dispatch) => {
  try {
    const user = await agent.User.login(values);

    dispatch(signIn(user));
    dispatch(setIsLogged(true));
    dispatch(setToken(user.token));
    localStorage.setItem("token", user.token);
    history.push("/orders");
  } catch (error) {
    if (typeof error.data == "string") {
      toast.error(`${error.data}`);
    } else {
      let errors = error.data.errors;
      for (const property in errors) {
        toast.error(`${error.data.errors[property]}`);
      }
    }
  }
};

export const getUserAsync = () => async (dispatch) => {
  try {
    const user = await agent.User.current();

    dispatch(signIn(user));
    dispatch(setIsLogged(true));
  } catch (error) {
    console.log(error);
  }
};

export const selectUser = (state) => state.auth.currentUser;
export const selectToken = (state) => state.auth.token;
export const selectIsLogged= (state) => state.auth.isLogged;

export default authSlice.reducer;
