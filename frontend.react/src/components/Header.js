import React from "react";
import { Link } from "react-router-dom";

const Header = () => {
  return (
    <div className="ui fixed menu">
      <div className="ui container center">
        <Link to={{ pathname: `/` }}>
          <h1 className="ui left">Swapfiets Bike Theft </h1>
        </Link>
      </div>
    </div>
  );
};

export default Header;
