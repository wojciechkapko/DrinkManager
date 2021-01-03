import {createSlice} from "@reduxjs/toolkit";


const commonSlice = createSlice({
    name: "common",
    initialState: {
        appLoaded: false
    },
    reducers:{
        setLoaded: (state) => {
            state.appLoaded = true;
        }
    }
});


export const { setLoaded } = commonSlice.actions;

export const selectAppLoaded = (state) => state.common.appLoaded;

export default commonSlice.reducer;