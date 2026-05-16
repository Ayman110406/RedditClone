export const createPost = async (title: string, body: string) => {
    const response = await fetch('/api/posts', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${localStorage.getItem('token')}` },
        body: JSON.stringify({ title, body })
    })
    return response
}
export const getPosts = async () => {
    const token = localStorage.getItem('token')
    const headers: HeadersInit = token ? { 'Authorization': `Bearer ${token}` } : {}
    const response = await fetch('/api/posts', { headers })
    return response
}
export const getPostById = async (id: string) => {
    const response = await fetch(`/api/posts/${id}`)
    return response
}
export const deletePost = async (id: string) => {
    const response = await fetch(`/api/posts/${id}`, {
        method: 'DELETE',
        headers: { 'Authorization': `Bearer ${localStorage.getItem('token')}` }
    })
    return response
}
export const votePost = async (id: string, value: number) => {
    const response = await fetch(`/api/posts/${id}/vote`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${localStorage.getItem('token')}` },
        body: JSON.stringify({ value })
    })
    return response
}