import {create} from 'zustand'
import { persist, createJSONStorage } from 'zustand/middleware'

type User={
    firstName: string
    lastName : string
    email : string
    role : string
} | null

type AuthStore ={
    user:User
    setUser: (u:User) => void
    clearUser : () => void
}

export const useAuthStore = create<AuthStore>()(
     persist(
    (set) => ({
      user: null,
      setUser: (u) => set({ user: u }),
      clearUser: () => set({ user: null }),
    }),
    {
      name: 'auth',
      storage: createJSONStorage(() => localStorage),
    }
  )
)