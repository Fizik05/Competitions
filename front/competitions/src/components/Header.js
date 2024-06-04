import React, { Component } from "react";
import { Link } from "react-router-dom";
import iconmain from "./img/iconmain.png"

class Header extends Component {
  render() {
    return(
      <header className="header">
        <div className='container'>
          <ul className="menu">
            <li className="menu-item"><Link to="/">Главная</Link></li>
            <li className="menu-item"><Link to="/coachespage">Для тренеров</Link></li>
            <li className="menu-item"><Link to="/competitions">Для студентов</Link></li>
            <a className="logo">
              <img className="logo-img" src={iconmain} alt="логотип сайта"/>
            </a>
            <a class="name">Соревнования и точка.</a>
          </ul>
        </div>
      </header>  
    )
  }
}
export default Header;
