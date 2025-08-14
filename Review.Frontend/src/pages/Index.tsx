import { GoogleLogin } from '@react-oauth/google'
import { useNavigate } from 'react-router-dom'
import { useReviews } from '@/hooks/useReviews'
import { useQueryClient } from '@tanstack/react-query'
import { useAuthStore } from '@/stores/useAuthStore'

export default function GoogleAuthApp() {
  const navigate = useNavigate()
  const setUser = useAuthStore((state) => state.setUser)

  const { loginWithGoogle, isLoggingIn } = useReviews()

  const onIdTokenSuccess = (resp: any) => {
    const idToken = resp?.credential
    if (!idToken) {
      console.error("No ID token from Google")
      return
    }

    loginWithGoogle(idToken, {
      onSuccess: (user) => {
        setUser(user)

        if (user.role === 'Admin') {
          navigate("/dashboard/admin")
        } else if (user.role === 'Student') {
          navigate("/dashboard/student/")
        } else {
          navigate("/no-access")
        }
      },
      onError: (e: any) => {
        if (e?.response?.status === 403) {
          navigate("/no-access")
        } else {
          console.error(e)
        }
      }
    })
  }

  return (
    <div>
      <GoogleLogin
        onSuccess={onIdTokenSuccess}
        onError={() => console.error("Login failed")}
        useOneTap
        auto_select
      />
      {isLoggingIn && <p>Logging in...</p>}
    </div>
  )
}
