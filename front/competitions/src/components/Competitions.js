import React, { Component } from 'react';

class Competitions extends Component {
  constructor(props) {
    super(props);
    this.state = {
      competitons: [],
      teams: {},
      loading: true,
      error: null,
    };
  }
 
  async componentDidMount() {
    await this.fetchCompetitons();
  }

  async fetchCompetitons() {
    try {
      const response = await fetch('https://localhost:7036/api/Competitions'); 
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      const data = await response.json();
      this.setState({ competitions: data, loading: false });

      for (var competition of data) {
        await this.fetchTeams(competition.id)
      }
    } catch (error) {
      this.setState({ error: error.message, loading: false });
    }
  }

  async fetchTeams(id) {
    try {
      const response = await fetch(`https://localhost:7036/api/Competitions/${id}/Teams`); 
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }

      const data = await response.json();
      var res = this.state.teams
      res[id] = data
      // res.push(data)

      this.setState({ teams: res , loading: false });
    } catch (error) {
      this.setState({ error: error.message, loading: false });
    }
  }
  

  render() {
    const { competitions, teams, loading, error} = this.state;

    if (loading) {
      return <div>Loading...</div>;
    }

    if (error) {
      return <div>Error: {error}</div>;
    }

  
    return (

      <div className='container'>
        <h1>Список соревнований</h1>
        <ul className='students' >
          {competitions.map(competition => (
            <li className='student-item' key={competition.id}>
              <p>{competition.name}</p>
               
              <p>Описание: {competition.description}</p>
              <p>Участвующие команды:</p>
              {teams[competition.id] ? (
              <ul>
                {teams[competition.id].map(team => (
                  <li key={team.id}>{team.name}</li>
                ))}
              </ul>
            ) : (
              <div>No teams for this competition.</div>
            )}
            </li>
          ))}
        </ul>
       
      </div>
    )
  }
}
  export default Competitions;

