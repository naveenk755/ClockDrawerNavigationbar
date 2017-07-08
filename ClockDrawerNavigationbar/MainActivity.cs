using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace ClockDrawerNavigationbar
{
    [Activity(Label = "ClockDrawerNavigationbar", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Android.Support.V4.Widget.DrawerLayout drawerLayout;
        Android.Support.V7.App.ActionBarDrawerToggle drawerToggle;
        ListView menuListView;

        Fragment[] fragments = new Fragment[]
        {
            new TimeFragment(),
            new StopwatchFragment(),
            new AboutFragment()
        };

        string[] titles = new string[] { "Time", "Stop Watch", "About" };

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
             SetContentView (Resource.Layout.Main);

            drawerLayout = FindViewById<Android.Support.V4.Widget.DrawerLayout>(Resource.Id.drawerLayout);

            drawerToggle = new Android.Support.V7.App.ActionBarDrawerToggle(this, drawerLayout, Resource.String.OpenNavDrawer, Resource.String.CloseNavDrawer);

            drawerLayout.AddDrawerListener(drawerToggle);

            ActionBar.SetDisplayHomeAsUpEnabled(true);

            menuListView = FindViewById<ListView>(Resource.Id.menuListView);
            menuListView.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, Android.Resource.Id.Text1, titles);
            menuListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e)=>OnMenuItemClick(e.Position);
            menuListView.SetItemChecked(0, true);
            OnMenuItemClick(0);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            drawerToggle.SyncState();
            base.OnPostCreate(savedInstanceState);
        }

        void OnMenuItemClick(int position)
        {
            base.FragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.frameLayout, fragments[position])
                .Commit();

            this.Title = titles[position];

            drawerLayout.CloseDrawer(menuListView);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (drawerToggle.OnOptionsItemSelected(item))
                return true;

            return base.OnOptionsItemSelected(item);
        }
    }
}

