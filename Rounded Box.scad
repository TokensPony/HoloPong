intersection(center = true ){ 
    cube([5, 6, 6], 3, center=true);
    //translate([1,0,0]){
        cube([5, 6, 6], 3, center=true);
    //}
    translate([-0.5,0,0]){
        scale([.33,1,1]){
            sphere(5, $fn = 50);
        }
    }
}