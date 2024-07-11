//using APPwithIdentity.Models.Entities;
//using APPwithIdentity.Service;
//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.AspNetCore.Components;

//namespace APPwithIdentity.Components.Pages
//{
//    public partial class CreateBlogPage
//    {
//        Blog newBlog = new Blog();

//        [Inject]
//        public IService blogService { get; set; }

//        [Inject]
//        public NavigationManager navigationManager { get; set; }

//        [Inject]
//        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

//        async Task CreateBlog()
//        {
//            try
//            {
//                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
//                var user = authState.User;

//                // Validate the form before proceeding
//                if (!IsValid(newBlog))
//                {
//                    // Handle invalid form state
//                    return;
//                }

//                await blogService.AddAsync(newBlog, user);
//                navigationManager.NavigateTo("/blogs");
//            }
//            catch (Exception ex)
//            {
//                // Handle exception appropriately
//                Console.WriteLine($"Error creating blog: {ex.Message}");
//            }
//        }

//        // Validate the Blog object before submission
//        private bool IsValid(Blog blog)
//        {
//            if (string.IsNullOrWhiteSpace(blog.Header) || string.IsNullOrWhiteSpace(blog.Text))
//            {
//                // Perform additional validation if needed
//                return false;
//            }
//            return true;
//        }
//    }
//}