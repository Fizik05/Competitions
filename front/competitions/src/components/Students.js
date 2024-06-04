import React, { Component } from 'react';

class Students extends Component {
  constructor(props) {
    super(props);
    this.state = {
      teams: [],
      students: [],
      kos:[],
      university: [],
      coaches: [],
      loading: true,
      error: null,
      newStudent: {
        name: '',
        surname: '',
        dateOfBirth: '',
        teamId: '',
      },
      newTeam: {
        name: '',
        kindOfSportId: '',
        universityId: '',
        coachId: '',
      },
    };
  }

  componentDidMount() {

    this.fetchStudents();
    this.fetchTeams();
    this.fetchKOS();
    this.fetchUniversity();
    this.fetchCoaches();
  }

  
  
//ЗАПРОС ДЛЯ ПОЛУЧЕНИЯ КОМАНДЫ
async fetchTeams() {
  try {
    const response = await fetch('https://localhost:7036/api/Teams'); 
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    const datateams = await response.json();
    this.setState({ teams: datateams, loading: false });
  } catch (error) {
    this.setState({ error: error.message, loading: false });
  }
}


//ЗАПРОСЫ СТУДЕНТОВ
  async fetchStudents() {
    try {
      const response = await fetch('https://localhost:7036/api/Students');
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      const data = await response.json();
      this.setState({students: data, loading: false });
    } catch (error) {
      this.setState({ error: error.message, loading: false });
    }
  }

  //ЗАПРОСЫ Видов Спорта
  async fetchKOS() {
    try {
      const response = await fetch('https://localhost:7036/api/KindOfSports');
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      const data = await response.json();
      this.setState({kos: data, loading: false });
    } catch (error) {
      this.setState({ error: error.message, loading: false });
    }
  }
  
   //ЗАПРОСЫ Университета
   async fetchUniversity() {
    try {
      const response = await fetch('https://localhost:7036/api/Universities');
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      const data = await response.json();
      this.setState({university: data, loading: false });
    } catch (error) {
      this.setState({ error: error.message, loading: false });
    }
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

  formatDate(dateString) {
    const options = { year: 'numeric', month: 'long', day: 'numeric' };
    return new Date(dateString).toLocaleDateString(undefined, options);
  }

//добавление
  //ИЗМЕНЕНИЕ
  handleInputChange = (event) => {
    const { name, value } = event.target;
    this.setState((prevState) => ({
      newStudent: {
        ...prevState.newStudent,
        [name]: value,
      },
    }));
  };
  handleInputChangeTeam = (event) => {
    const { name, value } = event.target;
    this.setState((prevState) => ({
      newTeam: {
        ...prevState.newTeam,
        [name]: value,
      },
    }));
  };
  //ДОБАВЛЕНИЕ СТУДЕНТА
  handleAddStudent = async () => {
    const { newStudent } = this.state;
    try {
      const response = await fetch('https://localhost:7036/api/Students', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newStudent),
      });
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      const addedStudent = await response.json();
      this.setState((prevState) => ({
        students: [...prevState.students, addedStudent],
        newStudent: { name: '', surname: '', dateOfBirth: '', teamId: '' }, // Сброс формы
      }));
    } catch (error) {
      this.setState({ error: error.message });
    }
  };
  handleAddTeam = async () => {
    const {newTeam} = this.state;
    try {
      const response = await fetch('https://localhost:7036/api/Teams', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newTeam),
      });
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      const addedTeam = await response.json();
      this.setState((prevState) => ({
        teams: [...prevState.teams, addedTeam],
        newTeam: { name: '', kindOfSportId: '', universityId: '', coachId: '' } , // Сброс формы
      }));
    } catch (error) {
      this.setState({ error: error.message });
    }
  };
  //УДАЛЕНИЕ СТУДЕНТА
  handleDeleteStudent = async (id) => {
    try {
      const response = await fetch(`https://localhost:7036/api/Students/${id}`, {
        method: 'DELETE',
      });
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      this.setState((prevState) => ({
        students: prevState.students.filter(student => student.id !== id),
      }));
    } catch (error) {
      this.setState({ error: error.message });
    }
  };

  render() {
    const { students, coaches, university, kos, loading, error, teams, newStudent,newTeam} = this.state;

    if (loading) {
      return <div>Loading...</div>;
    }

    if (error) {
      return <div>Error: {error}</div>;
    }

    return (
      <div className='container'>
        <h1>Список студентов</h1>
        <ul className='students'>
          {students.map(student => (
            <li  className='student-item' key={student.id}>
              {student.name} {student.surname} 
              <p>Дата Рождения: {this.formatDate(student.dateOfBirth)} </p>
              <p>Команда: {student.team.name}</p>
              <p>Тренер: {student.team.coach.name} {student.team.coach.surname}</p>
              <p>ВУЗ: {student.team.university.name} {student.team.university.surname}</p>
              <button onClick={() => this.handleDeleteStudent(student.id)}>Delete</button>
            </li>
          ))}
        </ul>
        <h1>Список команд</h1>
        
        <ul className='teams'>
          {teams.map(team => (
            <li className='team-item' key={team.id}>
              <p>"{team.name}"</p>
              <p>Секция: {team.kindOfSport.name}</p>
            </li>
          ))}
        </ul>
          <h2>Добавление нового студента</h2>
          <input
            type="text"
            name="name"
            value={newStudent.name}
            placeholder="Имя"
            onChange={this.handleInputChange}
          />
          <input
            type="text"
            name="surname"
            value={newStudent.surname}
            placeholder="Фамилия"
            onChange={this.handleInputChange}
          />
          <input
            type="text"
            name="dateOfBirth"
            value={newStudent.dateOfBirth}
            placeholder="Дата Рождения"
            onChange={this.handleInputChange}
          />
        <select
            name="teamId"
            value={newStudent.teamId}
            onChange={this.handleInputChange}
          >
            <option value="">Выбрать команду</option>
            {teams.map(team => (
              <option key={team.id} value={team.id}>{team.name}</option>
            ))}
          </select>
          <button onClick={this.handleAddStudent}>Добавить Студента</button>


          <h2>Добавление новой команды</h2>
          <input
            type="text"
            name="name"
            value={newTeam.name}
            placeholder="Название Команды"
            onChange={this.handleInputChangeTeam}
          />
          <select
            name="kindOfSportId"
            value={newTeam.kindOfSportId}
            onChange={this.handleInputChangeTeam}
          >
            <option value="">Выбрать вид спорта</option>
            {kos.map(kos => (
              <option key={kos.id} value={kos.id}>{kos.name}</option>
            ))}
          </select>
          
          <select
            name="universityId"
            value={newTeam.universityId}
            onChange={this.handleInputChangeTeam}
          >
            <option value="">Выбрать университет</option>
            {university.map(university => (
              <option key={university.id} value={university.id}>{university.name}</option>
            ))}
          </select>
          <select
            name="coachId"
            value={newTeam.coachId}
            onChange={this.handleInputChangeTeam}
          >
            <option value="">Кто тренерует?</option>
            {coaches.map(coach => (
              <option key={coach.id} value={coach.id}>{coach.name} {coach.surname}</option>
            ))}
          </select>
          <button onClick={this.handleAddTeam}>Добавить Команду</button>
          
      </div>
    );
  }
}

export default Students;
