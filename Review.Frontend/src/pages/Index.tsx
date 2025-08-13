import { useState } from 'react'
import { GoogleLogin } from '@react-oauth/google'
import { useNavigate } from 'react-router-dom'

const API_BASE_URL = 'http://localhost:7277/api'

type ApiUser = {
  firstName?: string
  lastName?: string
  email?: string
  role?: 'Admin' | 'Student' | string
}

type abc = {

}


export default function GoogleAuthApp() {
  const [user, setUser] = useState<ApiUser|null>(null)
  const [error, setError] = useState('')
  const navigate = useNavigate() 

  const onIdTokenSuccess = async (resp: any) => {
    try {
      const idToken = resp?.credential
      if (!idToken) throw new Error(`No ID token from Google`)

      const r = await fetch(`${API_BASE_URL}/auth/google`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        credentials: 'include',
        body: JSON.stringify({ IdToken: idToken })
      })

      if(r.status==403){
        navigate('/no-access')
        return
      }

      if (!r.ok) throw new Error(await r.text() || 'Auth failed')
      
      const data:ApiUser=await r.json()  
      setUser(data)
      if(data.role=='Admin')
      {
        navigate("/dashboard/admin")
      }
      else if(data.role=='Student')
      {
        navigate("/dashboard/student/")
      }
      else{
        navigate('/no-access')
      }
    } catch (e: any) { setError(e.message) }
  }

  return (
    <div>
      {!user ? (
        <>
          <GoogleLogin
            onSuccess={onIdTokenSuccess}
            onError={() => setError('Login failed')}
            useOneTap
            auto_select
          />
          {error && <p style={{ color: 'red' }}>{error}</p>}
        </>
      ) : (
        <div>Welcome, {user.firstName}</div>
      )}
    </div>
  )
}
