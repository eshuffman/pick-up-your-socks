import { useEffect, useState } from 'react'
import './App.css'

type Goal = {
  id: number
  title: string
  description: string | null
  isCompleted: boolean
  createdAt: string
  completedAt: string | null
}

function App() {
  const [goals, setGoals] = useState<Goal[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')
  const [title, setTitle] = useState('')
  const [description, setDescription] = useState('')
  const [saving, setSaving] = useState(false)

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

  const createGoal = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    setSaving(true)
    setError('')

    try {
      const response = await fetch('http://localhost:5160/api/goals', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          title,
          description: description || null,
        }),
      })

      if (!response.ok) {
        throw new Error('Failed to create goal.')
      }

      const newGoal: Goal = await response.json()

      setGoals((currentGoals) => [...currentGoals, newGoal])
      setTitle('')
      setDescription('')
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Something went wrong.')
    } finally {
      setSaving(false)
  }
}

  return (
    <div>
      <h1>Pick Up Your Socks!!!</h1>

      <form onSubmit={createGoal}>
        <h2>Add a Goal</h2>

        <div>
          <label htmlFor="title">Title</label>
          <input
            id="title"
            type="text"
            value={title}
            onChange={(event) => setTitle(event.target.value)}
            required
            maxLength={200}
          />
        </div>

        <div>
          <label htmlFor="description">Description (optional)</label>
          <textarea
            id="description"
            value={description}
            onChange={(event) => setDescription(event.target.value)}
          />
        </div>

        <button type="submit" disabled={saving}>
          {saving ? 'Saving...' : 'Add Goal'}
        </button>
      </form>

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