export interface GitHubRepository {
    id: number;
    name: string;
    full_name: string;
}

export type GitHubRepositoryResponse = GitHubRepository[];