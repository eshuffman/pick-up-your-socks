import { useEffect, useState } from 'react'
import './App.css'

function App() {
  const [goals, setGoals] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  useEffect(() => {
    fetch('http://localhost:5160/api/goals')
      .then((response) => {
        if (!response.ok) {
          throw new Error('Failed to fetch goals.')
        }

        return response.json()
      })
      .then((data) => {
        setGoals(data)
      })
      .catch((err) => {
        setError(err.message)
      })
      .finally(() => {
        setLoading(false)
      })
  }, [])

  return (
    <div>
      <h1>Pick Up Your Socks!!!</h1>

      {loading && <p>Loading goals...</p>}

      {error && <p>Error: {error}</p>}

      {!loading && !error && goals.length === 0 && (
        <p>You don't have any goals yet. Add one to get started!</p>
      )}

      {!loading && !error && goals.length > 0 && (
        <ul>
          {goals.map((goal) => (
            <li key={goal.id}>
              <strong>{goal.title}</strong>

              {goal.description && <p>{goal.description}</p>}

              <p>
                Status: {goal.isCompleted ? 'Completed' : 'In progress'}
              </p>
            </li>
          ))}
        </ul>
      )}
    </div>
  )
}

export default App