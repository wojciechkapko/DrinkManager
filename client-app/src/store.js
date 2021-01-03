import { configureStore } from "@reduxjs/toolkit";
import authReducer from "./slices/authSlice";
import commonReducer from "./slices/commonSlice";


export default configureStore({
  reducer: {
    common : commonReducer,
    auth: authReducer
  },
});
