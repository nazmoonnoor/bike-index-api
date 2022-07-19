import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import { uuid } from "uuidv4";
import api from "../api/cities";
import "./App.css";
import Header from "./Header";
import AddCity from "./City/AddCity";
import CityList from "./City/CityList";
import EditCity from "./City/EditCity";
import BikeTheft from "./Bike/BikeTheft";

function App() {
  const [cities, setCities] = useState([]);

  const retrieveCities = async () => {
    const response = await api.get("/city");
    return response.data;
  };

  const addCityHandler = async (city) => {
    console.log(city);
    const request = {
      id: uuid(),
      ...city,
    };

    const response = await api.post("/city", request);
    console.log(response);
    setCities([...cities, response.data]);
  };

  const updateCityHandler = async (city) => {
    const response = await api.put(`/city/${city.id}`, city);
    const { id, name, country } = response.data;
    setCities(
      cities.map((city) => {
        return city.id === id ? { ...response.data } : city;
      })
    );
  };

  const removeCityHandler = async (id) => {
    await api.delete(`/city/${id}`);
    const newCityList = cities.filter((city) => {
      return city.id !== id;
    });

    setCities(newCityList);
  };

  const getTheftCountHandler = async (city) => {
    console.log(city);
    const response = await api.get(`/bike-theft/count?city=${city}`);
    console.log(response);
    return response.data;
  };

  useEffect(() => {
    const getAllCities = async () => {
      const allCities = await retrieveCities();
      if (allCities) setCities(allCities);
    };

    getAllCities();
  }, []);

  useEffect(() => {}, [cities]);

  return (
    <div className="ui container">
      <Router>
        <Header />
        <Switch>
          <Route
            path="/city-list"
            exact
            render={(props) => (
              <CityList
                {...props}
                cities={cities}
                getCityId={removeCityHandler}
              />
            )}
          />
          <Route
            path="/city/add"
            render={(props) => (
              <AddCity {...props} addCityHandler={addCityHandler} />
            )}
          />

          <Route
            path="/city/edit"
            render={(props) => (
              <EditCity {...props} updateCityHandler={updateCityHandler} />
            )}
          />

          <Route
            path="/"
            render={(props) => (
              <BikeTheft
                {...props}
                getTheftCountHandler={getTheftCountHandler}
              />
            )}
          />
          <Route path="/bike-theft/:id" component={BikeTheft} />
        </Switch>
      </Router>
    </div>
  );
}

export default App;
