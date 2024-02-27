import Layout from "@theme/Layout";
import React, { useEffect } from "react";
import { GitHubRepository, GitHubRepositoryResponse } from "./models/GitHubRepository";
import useGitHubService from "@site/src/services/GitHubService";

export default function ProjectPage(): JSX.Element  {
	const [repos, setRepos] = React.useState<GitHubRepositoryResponse>([]);
	const githubService = useGitHubService();

	useEffect(() => {
		githubService.getAllRepos().then((response) => {
			setRepos(response.data);
		}).catch((error) => {
			console.error(error);
		});
	}, []);

	return (
		<Layout title="Hello" description="Hello React Page">
			<div className="row">
				{
					// repos.map((repo: GitHubRepository) => {
					// 	return (
					// 		<ProjectListing key={repo.id} name={repo.name} full_name={repo.full_name} id={repo.id} />
					// 	)
					// })
				}
			</div>
		</Layout>
	)
}
