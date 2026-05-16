import { useState, useEffect } from 'react'
import PostCard from '../components/PostCard'
import { getPosts, createPost } from '../services/PostService'
import styles from './Homepage.module.css'
import type { Post } from '../types'

function Homepage() {
    const [posts, setPosts] = useState<Post[]>([])
    const [username] = useState(localStorage.getItem('username') || 'Gebruiker')
    const [showModal, setShowModal] = useState(false)
    const [title, setTitle] = useState('')
    const [body, setBody] = useState('')
    const [isSubmitting, setIsSubmitting] = useState(false)
    const [error, setError] = useState('')

    const laadPosts = () => {
        getPosts()
            .then(res => res.json())
            .then(data => {
                if (data.success) setPosts(data.data)
            })
    }

    useEffect(() => {
        laadPosts()
    }, [])

    const handleCreatePost = async (e: React.FormEvent) => {
        e.preventDefault()
        if (!title.trim()) {
            setError('Vul een titel in')
            return
        }
        setIsSubmitting(true)
        setError('')
        try {
            const res = await createPost(title.trim(), body.trim())
            const data = await res.json()
            if (data.success) {
                setTitle('')
                setBody('')
                setShowModal(false)
                laadPosts()
            } else {
                setError(data.errorMessage || 'Er is iets misgegaan')
            }
        } catch {
            setError('Er is iets misgegaan')
        } finally {
            setIsSubmitting(false)
        }
    }

    const sluitModal = () => {
        setShowModal(false)
        setTitle('')
        setBody('')
        setError('')
    }

    return (
        <>
            <div className={styles.layout}>
                <div className={styles.sidebar}>
                    <div className={styles.welcomeCard}>
                        <p className={styles.welcomeText}>Welkom <span>{username}</span></p>
                        <button className={styles.createBtn} onClick={() => setShowModal(true)}>
                            Nieuwe post
                        </button>
                    </div>
                </div>

                <div className={styles.feed}>
                    {posts.map(post => (
                        <PostCard key={post.id} post={post} />
                    ))}
                    {posts.length === 0 && (
                        <p className={styles.emptyMsg}>Nog geen posts. Wees de eerste!</p>
                    )}
                </div>
            </div>

            {showModal && (
                <div className={styles.overlay} onClick={sluitModal}>
                    <div className={styles.modal} onClick={e => e.stopPropagation()}>
                        <div className={styles.modalHeader}>
                            <h2 className={styles.modalTitle}>Nieuwe post</h2>
                            <button className={styles.closeBtn} onClick={sluitModal}>✕</button>
                        </div>
                        <form onSubmit={handleCreatePost} className={styles.form}>
                            <input
                                className={styles.input}
                                type="text"
                                placeholder="Titel"
                                value={title}
                                onChange={e => setTitle(e.target.value)}
                                maxLength={300}
                            />
                            <textarea
                                className={styles.textarea}
                                placeholder="Tekst (optioneel)"
                                value={body}
                                onChange={e => setBody(e.target.value)}
                                rows={5}
                            />
                            {error && <p className={styles.errorMsg}>{error}</p>}
                            <div className={styles.formFooter}>
                                <button type="button" className={styles.cancelBtn} onClick={sluitModal}>
                                    Annuleren
                                </button>
                                <button type="submit" className={styles.submitBtn} disabled={isSubmitting}>
                                    {isSubmitting ? 'Plaatsen...' : 'Plaatsen'}
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </>
    )
}

export default Homepage
