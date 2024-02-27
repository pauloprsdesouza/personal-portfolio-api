import axios from "axios";

const useGitHubService = () => {
	const getAllRepos = async () => {
		return await axios.get('https://api.github.com/users/pauloprsdesouza/repos');
	};

	return {
		getAllRepos,
	};
}

export default useGitHubService;
