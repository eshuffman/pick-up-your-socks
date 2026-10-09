import { useState } from 'react'
import heroImg from './assets/hero.png'
import reactLogo from './assets/react.svg'
import viteLogo from './assets/vite.svg'
import './App.css'

function App() {
  const [apiPing, setApiPing] = useState("")

  const pingAPI = () => {
    fetch('http://localhost:5160/api/health')
      .then((response) => {
        if (!response.ok) {
          setApiPing("Response was not ok.")
        }
        return response.json(); // .NET typically returns JSON data
      })
      .then((response) => {
        setApiPing(JSON.stringify(response.data));
      })
      .catch((err) => {
        setApiPing(err.message);
      });
  };

  return (
    <div>
      <button
        onClick={pingAPI}
      >
        Hello!
      </button>

      <div>{apiPing}</div>
    </div>
  )
}

export default App
