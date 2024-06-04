import React, { Component } from 'react';
import artur from './img/artur.jpg'

class Coaches extends Component {
  constructor(props) {
    super(props);
    this.state = {
      coaches: [],
      loading: true,
      error: null,
    };
  }

  componentDidMount() {
    this.fetchCoaches();
  }

  async fetchCoaches() {
    try {
      const response = await fetch('https://localhost:7036/api/Coaches'); // Замените на фактический URL вашего другого API
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      const data = await response.json();
      this.setState({ coaches: data, loading: false });
    } catch (error) {
      this.setState({ error: error.message, loading: false });
    }
  }

  render() {
    const { coaches, loading, error} = this.state;

    if (loading) {
      return <div>Loading...</div>;
    }

    if (error) {
      return <div>Error: {error}</div>;
    }

    return (
      <div className='container'>
        <h1>Список Тренерского штаба</h1>
        <ul>
          {coaches.map(coach => (
            <li key={coach.id}>
              {coach.name}  {coach.surname}
              <img className="logo-img" src={artur} alt="логотип сайта"/>
            </li>
          ))}
        </ul>
        
      </div>
    )
  }

}
export default Coaches;