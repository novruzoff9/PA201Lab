import { AdminLayout } from '@/components/admin/AdminLayout';
import { useRestaurantStore } from '@/store/restaurantStore';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { FolderOpen, Package, TrendingUp, DollarSign } from 'lucide-react';

const Dashboard = () => {
  const categories = useRestaurantStore((state) => state.categories);
  const products = useRestaurantStore((state) => state.products);

  const totalRevenue = products.reduce((sum, p) => sum + p.price, 0);
  const avgPrice = products.length > 0 ? totalRevenue / products.length : 0;

  const stats = [
    {
      title: 'Kateqoriyalar',
      value: categories.length,
      icon: FolderOpen,
      color: 'bg-primary/10 text-primary',
    },
    {
      title: 'Məhsullar',
      value: products.length,
      icon: Package,
      color: 'bg-accent/10 text-accent',
    },
    {
      title: 'Orta qiymət',
      value: `₼${avgPrice.toFixed(2)}`,
      icon: TrendingUp,
      color: 'bg-emerald-500/10 text-emerald-600',
    },
    {
      title: 'Ümumi dəyər',
      value: `₼${totalRevenue.toFixed(2)}`,
      icon: DollarSign,
      color: 'bg-amber-500/10 text-amber-600',
    },
  ];

  return (
    <AdminLayout 
      title="Panel" 
      description="Restoran idarəetmə sisteminin ümumi görünüşü"
    >
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4 mb-8">
        {stats.map((stat, index) => (
          <Card key={stat.title} className="shadow-card border-border/50 animate-slide-up" style={{ animationDelay: `${index * 50}ms` }}>
            <CardHeader className="flex flex-row items-center justify-between pb-2">
              <CardTitle className="text-sm font-medium text-muted-foreground">
                {stat.title}
              </CardTitle>
              <div className={`w-9 h-9 rounded-lg flex items-center justify-center ${stat.color}`}>
                <stat.icon className="w-5 h-5" />
              </div>
            </CardHeader>
            <CardContent>
              <div className="text-2xl font-bold text-foreground">{stat.value}</div>
            </CardContent>
          </Card>
        ))}
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <Card className="shadow-card border-border/50">
          <CardHeader>
            <CardTitle className="text-lg">Son kateqoriyalar</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-3">
              {categories.slice(0, 5).map((category) => (
                <div 
                  key={category.id} 
                  className="flex items-center justify-between p-3 rounded-lg bg-muted/50"
                >
                  <div>
                    <p className="font-medium text-foreground">{category.name}</p>
                    <p className="text-sm text-muted-foreground">{category.description}</p>
                  </div>
                  <div className="text-sm text-muted-foreground">
                    {products.filter(p => p.categoryId === category.id).length} məhsul
                  </div>
                </div>
              ))}
              {categories.length === 0 && (
                <p className="text-center text-muted-foreground py-4">
                  Hələ kateqoriya yoxdur
                </p>
              )}
            </div>
          </CardContent>
        </Card>

        <Card className="shadow-card border-border/50">
          <CardHeader>
            <CardTitle className="text-lg">Son məhsullar</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-3">
              {products.slice(0, 5).map((product) => (
                <div 
                  key={product.id} 
                  className="flex items-center justify-between p-3 rounded-lg bg-muted/50"
                >
                  <div>
                    <p className="font-medium text-foreground">{product.name}</p>
                    <p className="text-sm text-muted-foreground">
                      {categories.find(c => c.id === product.categoryId)?.name}
                    </p>
                  </div>
                  <div className="font-semibold text-primary">
                    ₼{product.price.toFixed(2)}
                  </div>
                </div>
              ))}
              {products.length === 0 && (
                <p className="text-center text-muted-foreground py-4">
                  Hələ məhsul yoxdur
                </p>
              )}
            </div>
          </CardContent>
        </Card>
      </div>
    </AdminLayout>
  );
};

export default Dashboard;
