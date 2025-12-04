export type IUser = {
  id: string
  username?: string
  fullName?: string
  email?: string
  avatarUrl?: string
  role: RoleEnum
  status: UserStatus
  createdAt: string
  updatedAt: string
}

export enum AuthProvider {
  Local = 'Local',
  Google = 'Google',
  Facebook = 'Facebook',
}

export enum RoleEnum {
  Admin = 'Admin',
  Customer = 'Customer',
  Staff = 'Staff',
  Seller = 'Seller',
}

export enum UserStatus {
  Active = 'Active',
  Inactive = 'Inactive',
  Banned = 'Banned',
  Deleted = 'Deleted',
}
