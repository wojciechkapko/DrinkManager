import "./App.css";
import "./styles/global.min.css";
import Menu from "./pages/Menu";
import Orders from "./pages/Orders";
import TopNav from "./layout/Nav/TopNav";
import Login from "./pages/Login";
import DrinkDetails from "./pages/DrinkDetails/DrinkDetails";
import AdminHome from "./pages/Admin/AdminHome";
import Register from "./pages/Register";
import Notfound from "./layout/Notfound/Notfound";
import Unauthorized from "./layout/Unauthorized/Unauthorized";
import Home from "./pages/Home";
import { Switch, Route } from "react-router-dom";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import { ToastContainer } from "react-toastify";
import { useSelector } from "react-redux";
import { selectToken, getUserAsync } from "./slices/authSlice"
import {useEffect} from "react";
import {useDispatch} from "react-redux";
import { setLoaded, selectAppLoaded } from "./slices/commonSlice"
import Loading from "./layout/Loading/Loading";

const App = () => {
  const appLoaded = useSelector(selectAppLoaded);
  const token = useSelector(selectToken);
  const dispatch = useDispatch();
  useEffect(()=>{
    if(token){
      dispatch(getUserAsync()).finally(()=> dispatch(setLoaded()))
    }else{
      // todo: create AppLoaded prop
      dispatch(setLoaded());
    }
  },[token, dispatch])


  if(!appLoaded) return <Loading content="Loading..." />

  return (
    <Container className="p-3">
      <ToastContainer position="top-right" />
      <TopNav />
      <Row>
        <Col md={12}>
          <Switch>
            <Route path="/" exact>
              <Home />
            </Route>
            <Route path="/menu" component={Menu} />
            <Route path="/orders" component={Orders} />
            <Route path="/login">
              <Login />
            </Route>
            <Route path="/register">
              <Register />
            </Route>
            <Route path={"/drinkdetails/:id"}>
              <DrinkDetails />
            </Route>
            <Route path="/manager" component={AdminHome} />
            <Route path="/unauthorized" component={Unauthorized} />
            <Route component={Notfound} />
          </Switch>
        </Col>
      </Row>
    </Container>
  );
};

export default App;
