import {create} from 'zustand'

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