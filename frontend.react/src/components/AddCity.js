import React from "react";

class AddCity extends React.Component {
  state = {
    name: "",
    country: "",
  };

  add = (e) => {
    e.preventDefault();
    if (this.state.name === "" || this.state.country === "") {
      alert("ALl the fields are mandatory!");
      return;
    }
    this.props.addCityHandler(this.state);
    this.setState({ name: "", country: "" });
    this.props.history.push("/city-list");
  };
  render() {
    return (
      <div className="ui main">
        <h2>Add City</h2>
        <form className="ui form" onSubmit={this.add}>
          <div className="field">
            <label>Name</label>
            <input
              type="text"
              name="name"
              placeholder="Name"
              value={this.state.name}
              onChange={(e) => this.setState({ name: e.target.value })}
            />
          </div>
          <div className="field">
            <label>Country</label>
            <input
              type="text"
              name="country"
              placeholder="Country"
              value={this.state.country}
              onChange={(e) => this.setState({ country: e.target.value })}
            />
          </div>
          <button className="ui button blue">Add</button>
        </form>
      </div>
    );
  }
}

export default AddCity;
