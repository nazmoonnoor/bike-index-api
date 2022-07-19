import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import api from "../api/cities";

const BikeTheft = (props) => {
  const [location, setLocation] = useState("");
  const [theftCount, setTheftCount] = useState("");

  console.log(props);
  const name =
    props.location.state &&
    props.location.state.city &&
    props.location.state.city.name;

  useEffect(() => {
    setLocation(name);
    getTheftCountHandler(name);
  }, [name]);

  const getTheftCountHandler = async (location) => {
    setTheftCount("");
    const regexExp = /^((\-?|\+?)?\d+(\.\d+)?),\s*((\-?|\+?)?\d+(\.\d+)?)$/gi;
    let urlPart = regexExp.test(location)
      ? `latlng=${location}`
      : `city=${location}`;
    const response = await api.get(`/bike-theft/count?${urlPart}`);
    setTheftCount(response && response.data && response.data.count);
    return response.data;
  };

  const handleClick = (event) => {
    event.preventDefault();
    getTheftCountHandler(location);
  };

  return (
    <div className="ui main">
      <div className="ui form location">
        <div className="field">
          <label>Location</label>
          <input
            type="text"
            name="location"
            placeholder="City or lat/long"
            value={location}
            onChange={(event) => setLocation(event.target.value)}
          />
        </div>
        <div className="field">
          <label>Theft Count:</label>

          <h1>{theftCount}</h1>
        </div>
      </div>
      <button onClick={handleClick} className="ui button green left">
        Search
      </button>
      <Link to="/city-list">
        <button className="ui button blue right">City List</button>
      </Link>
    </div>
  );
};

export default BikeTheft;
