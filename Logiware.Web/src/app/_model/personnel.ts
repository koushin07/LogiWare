export interface Personnel{
  id: number,
  firstName: string,
  lastName: string
  role: string,
  code?: string
}

export interface CreatePersonnel {
  firstName: string,
  lastName: string,
  role: string,
  siteId: number
}
