import React from 'react';
import Giscus from "@giscus/react";
import { useColorMode } from '@docusaurus/theme-common';

const GiscusComponent: React.FC = () => {
	const { colorMode } = useColorMode();

	return (
		<Giscus
			repo="pauloprsdesouza/personal-portfolio-api"
			repoId="R_kgDOGnc1cQ"
			category="General"
			categoryId="DIC_kwDOGnc1cc4Cdk6g"
			mapping="pathname"
			strict="0"
			reactionsEnabled='1'
			emitMetadata='1'
			inputPosition="top"
			theme={colorMode}
			lang="en"
			loading="lazy"
		/>
	);
}

export default GiscusComponent; 