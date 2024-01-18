<template>
	<!------Header-------->
	<v-app-bar elevation="0" height="70">
		<div class="d-flex align-center justify-content-end w-100">
			<div>
				<v-tab to="/login">Login</v-tab>
			</div>
		</div>
	</v-app-bar>
	<!------Footer-------->
	<v-footer :app="true" :absolute="true">
		<LayoutFullFooter/>
	</v-footer>
</template>

<script setup>
// Data
</script>