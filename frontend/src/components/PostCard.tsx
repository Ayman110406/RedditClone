import { useState } from 'react'
import styles from './PostCard.module.css'
import type { Post } from '../types'
import { votePost } from '../services/PostService'

function PostCard({ post }: { post: Post }) {
    const [score, setScore] = useState(post.voteScore)
    const [userVote, setUserVote] = useState(post.userVote ?? 0)

    const handleVote = async (value: number) => {
        try {
            const res = await votePost(post.id, value)
            const data = await res.json()
            if (data.success) {
                setScore(data.data.newScore)
                setUserVote(data.data.userVote)
            }
        } catch {
            
        }
    }

    return (
        <div className={styles.card}>
            <div className={styles.voteCol}>
                <button
                    className={`${styles.voteBtn} ${userVote === 1 ? styles.voted : ''}`}
                    aria-label="voted"
                    onClick={() => handleVote(1)}
                >
                    ▲
                </button>
                <span className={styles.score}>{score}</span>
                <button
                    className={`${styles.voteBtn} ${userVote === -1 ? styles.voted : ''}`}
                    aria-label="voted"
                    onClick={() => handleVote(-1)}
                >
                    ▼
                </button>
            </div>
            <div className={styles.content}>
                <p className={styles.title}>{post.title}</p>
                <p className={styles.meta}>geplaatst door <span>u/{post.username}</span></p>
                <p className={styles.body}>{post.body}</p>
                <div className={styles.footer}>
                    <span>{post.commentCount} reacties</span>
                </div>
            </div>
        </div>
    )
}

export default PostCard
