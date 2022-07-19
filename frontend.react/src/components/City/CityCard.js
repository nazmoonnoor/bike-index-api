import React from "react";
import { Link } from "react-router-dom";
import bike from "../../images/bike.png";

const CityCard = (props) => {
  const { id, name, country } = props.city;
  return (
    <div className="item">
      <img className="ui avatar image" src={bike} alt="bike" />
      <div className="content">
        <Link to={{ pathname: `/city/${id}`, state: { city: props.city } }}>
          <div className="header">{name}</div>
          <div>{country}</div>
        </Link>
      </div>
      <i
        className="trash alternate outline icon"
        style={{ color: "red", marginTop: "7px", marginLeft: "10px" }}
        onClick={() => props.clickHander(id)}
      ></i>
      <Link to={{ pathname: `/city/edit`, state: { city: props.city } }}>
        <i
          className="edit alternate outline icon"
          style={{ color: "blue", marginTop: "7px" }}
        ></i>
      </Link>
    </div>
  );
};

export default CityCard;
